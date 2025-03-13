open System
// 2/1Получить список из минимальных цифр натуральных чисел, содержащихся висходном списке 


// Функция для получения списка
let readListFromConsole () =
    let rec loop acc =
        printf "Введите элемент (или оставьте пустым для завершения): "
        let input = Console.ReadLine()
        if input = "" then
            List.rev acc // возвращает новый список с элементами в обратном порядке
        else
            // Проверяем, является ли введенное значение числом
            match Int32.TryParse(input) with
            | (true, number) when number > 0 -> // Проверяем, что число натуральное (больше 0)
                loop (number :: acc) // Добавляем введенное число к аккумулятору
            | (false, _) ->
                printfn "Ошибка: '%s' не является числом." input
                loop acc // продолжаем ввод
    loop []

// Функция для вывода списка
let printList list =
    printfn "Содержимое списка:"
    list |> List.iter (fun element -> printfn "- %s" element) // применяет к кождому эелементу

// нахождение минимальной цифры
let rec findMinDigit (number: int) (acc: int) =
    if number = 0 then
        acc
    else
        let digit = number % 10 // Получаем последнюю цифру
        let newMin = if digit < acc then digit else acc // Обновляем минимальную цифру
        findMinDigit (number / 10) newMin // Рекурсивный вызов, убирая последнюю цифру


// Функция для нахождения минимальных цифр в списке чисел
let findMinDigitsInList (numbers: int list) =
    List.map (fun n -> findMinDigit n 9) numbers

// Главная программа
[<EntryPoint>]
let main argv =
    let numbers = readListFromConsole() // Читаем список чисел из консоли
    //printList numbers // Выводим числа
    let minDigits = findMinDigitsInList numbers // Находим минимальные цифры
    printfn "Минимальные цифры в каждом числе: %A" minDigits // Выводим результаты
    0 // Возвращаем код завершения
    let minD = minDList number

    printfn "Минимальные цифры для введенных чисел: %A" minD

    0 // для завершения программы
