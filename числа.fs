open System

// Определение типа двоичного дерева
type Tree =
    | Leaf of int
    | Node of int * Tree * Tree

// Функция для создания сбалансированного дерева
let rec buildBalancedTree (elements: int list) : Tree option =
    match elements with
    | [] -> None
    | [x] -> Some(Leaf x)
    | _ ->
        let mid = elements.Length / 2
        let left = buildBalancedTree elements.[..mid-1]
        let right = buildBalancedTree elements.[mid+1..]
        Some(Node(elements.[mid], 
                 left |> Option.defaultValue (Leaf 0), 
                 right |> Option.defaultValue (Leaf 0)))


// Функция для нахождения суммы чётных значений в листьях (используем fold)
let sumEvenLeaves tree =
    let folder state = function
        | Leaf n when n % 2 = 0 -> state + n
        | _ -> state
    match tree with
    | None -> 0
    | Some t ->
        let rec foldTree f acc tree =
            match tree with
            | Leaf x -> f acc (Leaf x)
            | Node (x, left, right) ->
                let accAfterLeft = foldTree f acc left
                let accAfterNode = f accAfterLeft (Node (x, Leaf 0, Leaf 0)) // Фиктивные листья, так как нам нужны только настоящие листья
                foldTree f accAfterNode right
        foldTree folder 0 t

// Функции для визуализации дерева
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
        | Leaf n -> matrix.[row, col] <- n.ToString()
        | Node (n, left, right) ->
            matrix.[row, col] <- n.ToString()
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

// Функция для получения количества элементов и самих элементов
let getUserInput () =
    printf "Введите количество элементов (1-15): "
    let count = Console.ReadLine() |> int
    
    if count < 1 || count > 15 then
        printfn "Ошибка: количество должно быть от 1 до 15"
        None
    else
        printfn "Введите %d чисел через Enter:" count
        let elements = 
            [for _ in 1..count -> Console.ReadLine() |> int]
            |> List.sort
        Some elements


// Функция для обработки дерева и вывода данных
let processTree elements =
    let tree = buildBalancedTree elements
    printfn "\nИсходное дерево:"
    tree |> Option.iter (treeToMatrix >> print2DMatrix)
    
    let sumEven = sumEvenLeaves tree
    printfn "\nСумма чётных значений в листьях: %d" sumEven

    tree // Вернуть дерево для возможного дальнейшего использования


[<EntryPoint>]
let main _ =
    try
        match getUserInput () with
        | None -> 1
        | Some elements -> 
            let tree = processTree elements

            0
    with
    | :? FormatException -> 
        printfn "Ошибка ввода числа"
        1
    | ex -> 
        printfn "Произошла ошибка: %s" ex.Message
        1
