open System

// Функция для получения списка из консоли лаба 3/1
let readListFromConsole () =
    let rec loop acc =
        printf "Введите элемент (или оставьте пустым для завершения): "
        let input = Console.ReadLine()
        if input = "" then
            Seq.rev acc // возвращает новый список с элементами в обратном порядке
        else
            // Проверяем, является ли введенное значение числом
            match Int32.TryParse(input) with
            | (true, number) when number > 0 -> // Проверяем, что число натуральное (больше 0)
                loop (number :: acc) // Добавляем введенное число к аккумулятору
            | (true, number) when number <= 0 ->
                printfn "Ошибка: '%s' не является натуральным числом." input
                loop acc // Продолжаем ввод
            | (false, _) ->
                printfn "Ошибка: '%s' не является числом." input
                loop acc // продолжаем ввод
    loop []


// Функция для вывода списка
let printList seq =
    printfn "Содержимое списка:"
    seq |> Seq.iter (fun element -> printfn "- %d" element) // Выводим число

// Нахождение минимальной цифры
let rec findMinDigit (number: int) (acc: int) =
    if number = 0 then
        acc
    else
        let digit = number % 10 // Получаем последнюю цифру
        let newMin = if digit < acc then digit else acc // Обновляем минимальную цифру
        findMinDigit (number / 10) newMin // Рекурсивный вызов, убирая последнюю цифру

// Функция для нахождения минимальных цифр в последовательности чисел
let findMinDigitsInSeq (numbers: seq<int>) =
    numbers |> Seq.map (fun n -> findMinDigit n 9)

// Главная программа
[<EntryPoint>]
let main argv =
    let numbers = readListFromConsole() // Читаем список чисел из консоли
    // printList numbers // Выводим числа
    let minDigits = findMinDigitsInSeq numbers // Находим минимальные цифры
    printfn "Минимальные цифры в каждом числе: %A" minDigits // Выводим результаты
    0 // Возвращаем код завершения
