use super::command::{Category, Command};
use super::userdata::UserData;
use futures::{stream::FuturesUnordered, StreamExt};
use std::collections::HashMap;

struct UserStats {
    id: i32,
    older_periods: u32,
    preceding_period: u32,
    current_period: u32,
    subsequent_period: u32,
    future_periods: u32,
}

pub async fn filter_users(
    command: &Command,
    data_by_user: HashMap<i32, Vec<UserData>>,
) -> Vec<i32> {
    data_by_user
        .into_iter()
        .map(async move |user_and_data| {
            user_and_data.1.into_iter().fold(
                UserStats {
                    id: user_and_data.0,
                    older_periods: 0,
                    preceding_period: 0,
                    current_period: 0,
                    subsequent_period: 0,
                    future_periods: 0,
                },
                |mut user_stats, data| {
                    if data.date > command.to_date + command.get_span() {
                        user_stats.future_periods += data.meals;
                    } else if data.date > command.to_date
                        && data.date <= command.to_date + command.get_span()
                    {
                        user_stats.subsequent_period += data.meals;
                    } else if data.date >= command.from_date && data.date <= command.to_date {
                        user_stats.current_period += data.meals;
                    } else if data.date < command.from_date
                        && data.date >= command.from_date - command.get_span()
                    {
                        user_stats.preceding_period += data.meals;
                    } else if data.date < command.from_date - command.get_span() {
                        user_stats.older_periods += data.meals;
                    }

                    user_stats
                },
            )
        })
        .collect::<FuturesUnordered<_>>()
        .collect::<Vec<_>>()
        .await
        .into_iter()
        .filter_map(|user_stats| match command.category {
            Category::Bored => get_id_if_valid(
                |s| s.current_period < 5 && s.preceding_period >= 5,
                user_stats,
            ),
            Category::Active => get_id_if_valid(|s| s.current_period >= 5, user_stats),
            Category::SuperActive => get_id_if_valid(|s| s.current_period > 10, user_stats),
        })
        .collect()
}

fn get_id_if_valid<F>(f: F, stats: UserStats) -> Option<i32>
where
    F: FnOnce(&UserStats) -> bool,
{
    if f(&stats) {
        Some(stats.id)
    } else {
        None
    }
}
