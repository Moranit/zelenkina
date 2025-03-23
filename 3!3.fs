open System
open System.IO

let rec getDirectoryPath () =
    printfn "Введите путь к каталогу:"
    let directoryPath = Console.ReadLine() 
    if Directory.Exists(directoryPath) then
        directoryPath 
    else
        printfn "Каталог не существует! Пожалуйста, попробуйте снова."
        getDirectoryPath() 

let findShortestFileName (directoryPath: string) =
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
    let directoryPath = getDirectoryPath() 
    findShortestFileName directoryPath
    0
