open System

// Получение последовательности
let readSequenceFromConsole () =
    let rec loop acc =
        printf "Введите элемент (или оставьте пустым для завершения): "
        let input = Console.ReadLine()
        if input = "" then
            Seq.rev acc // Возвращает новую последовательность с элементами в обратном порядке
        else
            // Проверяем, является ли введенное значение числом
            match Int32.TryParse(input) with
            | (true, number) when number > 0 -> // Проверяем, что число натуральное (больше 0)
                loop (number :: acc) // Добавляем введенное число к аккумулятору
            | (false, _) ->
                printfn "Ошибка: '%s' не является числом." input
                loop acc // Продолжаем ввод
            | _ -> 
                printfn "Некорректное число."
                loop acc // Продолжаем ввод
    loop [] 

// Функция для вывода последовательности
let printSequence seq =
    printfn "Содержимое последовательности:"
    seq |> Seq.iter (fun element -> printfn "- %d" element) // Для каждого элемента

// Функция для проверки, содержит ли число цифру
let rec containsDigit (number: int) (digit: int) : bool =
    if number = 0 then 
        false // Если число равно 0, цифра не найдена
    else
        let currentDigit = number % 10 // Получаем последнюю цифру
        if currentDigit = digit then 
            true // Если последняя цифра равна искомой, возвращаем true
        else 
            containsDigit (number / 10) digit // Рекурсивный вызов для оставшейся части числа

// Функция для подсчета количества элементов, содержащих заданную цифру
let countElementsWithDigit (numbers: seq<int>) (digit: int) =
    Seq.fold (fun acc number -> if containsDigit number digit then acc + 1 else acc) 0 numbers

// Функция для ввода натурального числа
let readNaturalNumberFromConsole () =
    let rec loop () =
        printf "Введите натуральное число (или оставьте пустым для завершения): "
        let input = Console.ReadLine()
        if input = "" then
            None // Возвращаем None, если ввод пустой
        else
            match Int32.TryParse(input) with
            | (true, number) when number > 0 -> Some number // Если число натуральное, возвращаем его
            | (true, _) -> 
                printfn "Ошибка: '%s' не является натуральным числом." input
                loop () // Продолжаем ввод
            | _ -> 
                printfn "Некорректный ввод. Введите целое число."
                loop () // Продолжаем ввод
    loop () // Запускаем цикл ввода

// Главная программа
[<EntryPoint>]
let main argv =
    let numbers = readSequenceFromConsole() // Читаем последовательность натуральных чисел из консоли
    printSequence numbers // Выводим последовательность
    printf "Введите цифру для поиска: "
    let digitInput = Console.ReadLine()

    match Int32.TryParse(digitInput) with
    | (true, digit) when digit >= 0 && digit <= 9 -> // Проверяем, что цифра от 0 до 9
        let count = countElementsWithDigit numbers digit
        printfn "Количество элементов, содержащих цифру %d: %d" digit count
    | _ ->
        printfn "Ошибка: '%s' не является цифрой." digitInput

    0 // Возвращаем 0 для завершения программы
