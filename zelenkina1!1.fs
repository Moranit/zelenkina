open System
let stepeni n = //определяем функцию
    [ for i in 0 .. n -> pown 2 i ]
//pown-два аргумента (основание и n)
//выполнение программы
[<EntryPoint>]
let main argv =
    printfn "Введите степень:"
    let input = Console.ReadLine()
    match Int32.TryParse(input) with
    | (true, n) when n >= 0 -> 
        let powersOfTwo = stepeni n
        printfn "Степени числа 2 от 0 до %d: %A" n powersOfTwo
    | _ ->
        printfn "Ввод должен быть неотрицательным целым числом."
    0 //завершение программы