use super::jsondata;
use chrono::prelude::*;
pub struct UserData {
    pub user_id: i32,
    pub date: NaiveDate,
    pub meals: u32,
}

pub async fn data_from_file(path: &str) -> anyhow::Result<Vec<UserData>> {
    jsondata::load_json(path)
        .await?
        .calendar
        .days_with_details
        .into_values()
        .map(|day_with_detail| {
            Ok(UserData {
                user_id: day_with_detail.day.user_id,
                date: NaiveDate::parse_from_str(day_with_detail.day.date.as_str(), "%Y-%m-%d")?,
                meals: day_with_detail.details.meals_with_details.len() as u32,
            })
        })
        .collect::<Vec<anyhow::Result<UserData>>>()
        .into_iter()
        .collect()
}
