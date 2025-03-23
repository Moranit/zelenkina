open System
open System.IO

let findShortestFileName (directoryPath: string) =
    if not (Directory.Exists(directoryPath)) then
        printfn "Каталог не существует."
    else
        let files = Directory.GetFiles(directoryPath)
        if files.Length = 0 then
            printfn "В каталоге нет файлов."
        else
            let shortestFileName =
                files
                |> Seq.map Path.GetFileName // Получаем последовательность названий файлов
                |> Seq.minBy String.length // Ищем самое короткое название файла
            printfn "Самое короткое название файла: %s" shortestFileName

[<EntryPoint>]
let main argv =
    // Укажите путь к каталогу
    let directoryPath = "C:\лабы"
    findShortestFileName directoryPath
    0
