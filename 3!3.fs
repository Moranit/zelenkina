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
                |> Seq.map Path.GetFileName 
                |> Seq.minBy String.length 
            printfn "Самое короткое название файла: %s" shortestFileName

[<EntryPoint>]
let main argv =
    printfn "Введите путь к каталогу:"
    let directoryPath = Console.ReadLine() 
    findShortestFileName directoryPath
    0

