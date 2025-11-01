use async_std::fs::read_to_string;
use async_std::path::Path;
use serde::{Deserialize, Serialize};
use std::collections::HashMap;

#[derive(Serialize, Deserialize, Clone)]
pub struct Root {
    pub calendar: Calendar,
}

#[derive(Serialize, Deserialize, Clone)]
#[serde(rename_all = "camelCase")]
pub struct Calendar {
    pub days_with_details: HashMap<String, DayWithDetail>,
}

#[derive(Serialize, Deserialize, Clone)]
pub struct DayWithDetail {
    pub day: Day,
    pub details: Details,
}

#[derive(Serialize, Deserialize, Clone)]
#[serde(rename_all = "camelCase")]
pub struct Day {
    pub id: i32,
    pub user_id: i32,
    pub date: String,
}

#[derive(Serialize, Deserialize, Clone)]
#[serde(rename_all = "camelCase")]
pub struct Details {
    pub meals_with_details: HashMap<String, MealAndDetail>,
}

#[derive(Serialize, Deserialize, Clone)]
pub struct MealAndDetail {
    pub meal: Meal,
}

#[derive(Serialize, Deserialize, Clone)]
pub struct Meal {
    pub id: i32,
}

pub async fn load_json(filename: &str) -> anyhow::Result<Root> {
    let file = read_to_string(Path::new(format!("./{}", filename).as_str())).await?;
    let json_data: Root = serde_json::from_str(file.as_str())?;
    Ok(json_data)
}
