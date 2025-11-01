mod filedata;
mod jsondata;

use filedata::data_from_file;
pub use filedata::UserData;
use futures::{stream::FuturesUnordered, StreamExt};

use std::collections::HashMap;

pub async fn data_by_user_from_files(
    file_paths: Vec<String>,
) -> anyhow::Result<HashMap<i32, Vec<UserData>>> {
    Ok(file_paths
        .iter()
        .map(async move |path| data_from_file(path).await)
        .collect::<FuturesUnordered<_>>()
        .collect::<Vec<_>>()
        .await
        .into_iter()
        .collect::<anyhow::Result<Vec<Vec<UserData>>>>()?
        .into_iter()
        .flatten()
        .fold(
            HashMap::<i32, Vec<UserData>>::new(),
            |mut grouped_by_user, user_data| {
                grouped_by_user
                    .entry(user_data.user_id)
                    .or_insert_with(Vec::new)
                    .push(user_data);
                grouped_by_user
            },
        ))
}
