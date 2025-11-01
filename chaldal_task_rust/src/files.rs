use glob::glob;

pub async fn get_filenames(directory: &str, filetype: &str) -> anyhow::Result<Vec<String>> {
    let pattern = format!("{}/*.{}", directory, filetype);
    glob(pattern.as_str())?
        .map(|pathbuf| {
            Ok(pathbuf?
                .to_str()
                .ok_or_else(|| anyhow::anyhow!("path could not be converted to &str"))?
                .to_string())
        })
        .collect()
}
