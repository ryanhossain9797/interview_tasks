use chrono::{prelude::*, Duration};
use std::env;

pub enum Category {
    Bored,
    Active,
    SuperActive,
}

pub struct Command {
    pub category: Category,
    pub from_date: NaiveDate,
    pub to_date: NaiveDate,
}

impl Command {
    pub fn get_span(&self) -> Duration {
        if self.to_date > self.from_date {
            self.to_date - self.from_date
        } else {
            self.from_date - self.to_date
        }
    }
}

pub fn get_command() -> anyhow::Result<Command> {
    let args: Vec<String> = env::args().collect();

    if args.len() != 4 {
        return Err(anyhow::anyhow!(
            "Wrong number of arguments. required 3, got {}",
            args.len() - 1
        ));
    }

    Ok(Command {
        category: get_category(args[1].as_str())?,
        from_date: NaiveDate::parse_from_str(args[2].as_str(), "%Y-%m-%d")?,
        to_date: NaiveDate::parse_from_str(args[3].as_str(), "%Y-%m-%d")?,
    })
}

fn get_category(category: &str) -> anyhow::Result<Category> {
    match category {
        "bored" => Ok(Category::Bored),
        "active" => Ok(Category::Active),
        "superactive" => Ok(Category::SuperActive),
        _ => Err(anyhow::anyhow!("Wrong category {}", category)),
    }
}
