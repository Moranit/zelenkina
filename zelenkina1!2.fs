open System

// Функция для нахождения первой цифры натурального числа
let firstDigit (n: int) =
    if n <= 0 then
        None // Возвращаем None, если число не натуральное
    else
        let numberString = n.ToString() // Преобразуем число в строку
        Some (numberString.[0]) // Берем первый символ строки и возвращаем его

// Основная программа
[<EntryPoint>]
let main argv =
    printfn "Введите натуральное число:"
    let input = Console.ReadLine()

    match Int32.TryParse(input) with
    | (true, n) when n > 0 -> // Если введенное значение - натуральное число
        match firstDigit n with
        | Some digit ->
            printfn "Первая цифра числа %d: %c" n digit
        | None -> 
            printfn "Ошибка: Некорректное значение."
    | _ ->
        printfn "Пожалуйста, введите корректное натуральное число."

    0 // Возвращаем код завершения программы
