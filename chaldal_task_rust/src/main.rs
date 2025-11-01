#![feature(async_closure)]
mod command;
mod files;
mod filter;
mod userdata;

#[async_std::main]
async fn main() -> anyhow::Result<()> {
    let start = std::time::Instant::now();
    let file_paths = files::get_filenames("./data", "json").await?;
    let command = command::get_command()?;

    let loaded_data = userdata::data_by_user_from_files(file_paths).await?;

    println!(
        "{}",
        filter::filter_users(&command, loaded_data)
            .await
            .into_iter()
            .fold("".to_string(), |mut comma_separated, id| {
                comma_separated += format!(", {}", id).as_str();
                comma_separated
            })
            .trim_start_matches(", ")
    );
    println!("{} secs", start.elapsed().as_secs_f64());
    Ok(())
}
