open System

// Определение типа двоичного дерева
type Tree =
    | Leaf of string
    | Node of string * Tree * Tree

// Функция для создания сбалансированного дерева
let rec buildBalancedTree (elements: string list) : Tree option =
    match elements with
    | [] -> None
    | [x] -> Some(Leaf x)
    | _ ->
        let mid = elements.Length / 2
        let left = buildBalancedTree elements.[..mid-1]
        let right = buildBalancedTree elements.[mid+1..]
        Some(Node(elements.[mid], 
                 left |> Option.defaultValue (Leaf " "), 
                 right |> Option.defaultValue (Leaf " ")))

// Функция для замены последнего символа
let replaceLastChar (str: string) (ch: char) =
    if str.Length = 0 then str
    else str.Substring(0, str.Length - 1) + string ch

// Функция для преобразования дерева
let rec replaceLastInTree tree ch =
    match tree with
    | Leaf str -> Leaf (replaceLastChar str ch)
    | Node (str, left, right) ->
        Node (replaceLastChar str ch, replaceLastInTree left ch, replaceLastInTree right ch)

// Функции для визуализации
let findHeight tree =
    let rec loop depth = function
        | Leaf _ -> depth
        | Node (_, left, right) -> max (loop (depth + 1) left) (loop (depth + 1) right)
    loop 0 tree

let treeToMatrix tree =
    let height = findHeight tree
    let rows = height + 1
    let cols = pown 2 (height + 1) - 1
    let matrix = Array2D.create rows cols " "

    let rec fillMatrix row col = function
        | Leaf str -> matrix.[row, col] <- if String.IsNullOrEmpty(str) then " " else str
        | Node (str, left, right) ->
            matrix.[row, col] <- str
            let offset = pown 2 (height - row - 1)
            fillMatrix (row + 1) (col - offset) left
            fillMatrix (row + 1) (col + offset) right

    fillMatrix 0 ((cols - 1) / 2) tree
    matrix

let print2DMatrix matrix =
    for row in 0 .. Array2D.length1 matrix - 1 do
        for col in 0 .. Array2D.length2 matrix - 1 do
            printf "%s" matrix.[row, col]
        printfn ""

// Функция для получения элементов от пользователя
let getUserInput () =
    printf "Введите количество элементов (1-15): "
    let count = Console.ReadLine() |> int
    
    if count < 1 || count > 15 then
        printfn "Ошибка: количество должно быть от 1 до 15"
        None
    else
        printfn "Введите %d строк через Enter:" count
        let elements = 
            [for _ in 1..count -> Console.ReadLine()]
            |> List.sort
        Some elements

// Функция для обработки и отображения дерева
let processTree elements =
    match buildBalancedTree elements with
    | None -> 
        printfn "Не удалось построить дерево"
    | Some tree ->
        printfn "\nВведите символ для замены:"
        let ch = Console.ReadKey().KeyChar
        Console.WriteLine()
        
        printfn "\nИсходное дерево:"
        treeToMatrix tree |> print2DMatrix
        
        let transformedTree = replaceLastInTree tree ch
        
        printfn "\nПреобразованное дерево:"
        treeToMatrix transformedTree |> print2DMatrix

// Основная функция
[<EntryPoint>]
let main _ =
    try
        match getUserInput () with
        | None -> 1
        | Some elements -> 
            processTree elements
            
            0
    with
    | :? FormatException -> 
        printfn "Ошибка ввода числа"
        1
    | ex -> 
        printfn "Ошибка: %s" ex.Message
        1
