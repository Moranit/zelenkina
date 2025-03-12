open System

// 3 лаба - добавление, удаление, поиск, объед списков

// Функция для добавления элемента в список
let addElement element list = 
    element :: list

// Функция для удаления элемента из списка
let removeElement element list =
    List.filter (fun x -> x <> element) list //проходит по списку и возвращает новый список с удаленным элементом

// Функция для поиска элемента в списке
let findElement element list =
    List.exists (fun x -> x = element) list// возвращает тру ксли хотя бы один Элемент удовл усл

// Функция для сцепки двух списков
let concatenateLists list1 list2 =
    list1 @ list2 //оператор для сцепления двух списков

// Функция для получения элемента по индексу
let getElementAtIndex index list = //ПРоверям в диапозоне ли Элемент
    if index < 0 || index >= List.length list then
        None // Возвращаем None, если индекс вне диапазона
    else
        Some (List.item index list)

// Функция для ввода индекса
let readIndexFromConsole () =
    let rec loop acc = 
        printf "Введите индекс элемента(или оставьте пустым для завершения):"
        let input = Console.ReadLine()
        if input = "" then
            List.rev acc // Возвращаем перевернутый список индексов
        else
            match Int32.TryParse(input) with
            | (true, index) -> loop (index :: acc) // Добавляем индекс к аккумулятору
            | _ -> 
                printfn "Некорректный ввод. Индекс должен быть целым числом."
                loop acc // Повторяем ввод без изменений в аккумуляторе
    loop []

// Функция для получения списка
let readListFromConsole () =
    let rec loop acc =
        printf "Введите элемент (или оставьте пустым для завершения): "
        let input = Console.ReadLine()
        if input = "" then
            List.rev acc // Возвращаем перевернутый список, чтобы сохранить порядок ввода
        else
            loop (input :: acc) // Добавляем введенный элемент к аккумулятору
    loop []

// Функция для вывода списка
let printList list =
    printfn "Содержимое списка:"
    list |> List.iter (fun element -> printfn "- %s" element)

// Основное меню
let rec showMenu list =
    printfn "\nВыберите действие:"
    printfn "1. Вывести список"
    printfn "2. Получить элемент по индексу"
    printfn "3. Ввести новый список"
    printfn "4. Добавить элемент в список"
    printfn "5. Удалить элемент из списка"
    printfn "6. Найти элемент в списке"
    printfn "7. Сцепить списки"
    printfn "8. Выход"

    
    let choice = Console.ReadLine()
    match choice with
    | "1" -> 
        printList list
        showMenu list // Возвращаемся в меню
    | "2" -> 
        let indexes = readIndexFromConsole ()
        // Вывод элементов по введенным индексам
        List.iter (fun index ->
            match getElementAtIndex index list with
            | Some element -> printfn "Элемент по индексу %d: %s" index element
            | None -> printfn "Индекс %d вне диапазона" index
        ) indexes
        showMenu list // Возвращаемся в меню
    | "3" ->
        let newList = readListFromConsole ()
        showMenu newList // Переходим в меню с новым списком
    | "4" -> 
        printf "Введите элемент для добавления: "
        let element = Console.ReadLine()
        let updatedList = addElement element list
        printfn "Элемент \"%s\" добавлен." element
        showMenu updatedList // Переходим в меню с обновленным списком
    | "5" ->
        printf "Введите элемент для удаления: "
        let element = Console.ReadLine()
        let updatedList = removeElement element list
        printfn "Элемент \"%s\" удалён." element
        showMenu updatedList // Переходим в меню с обновленным списком
    | "6" ->
        printf "Введите элемент для поиска: "
        let element = Console.ReadLine()
        if findElement element list then
            printfn "Элемент \"%s\" найден в списке." element
        else
            printfn "Элемент \"%s\" не найден в списке." element
        showMenu list // Возвращаемся в меню
    | "7" ->
        printfn "Введите новый список для сцепления:"
        let newList = readListFromConsole ()
        let concatenatedList = concatenateLists list newList
        printfn "Списки сцеплены."
        showMenu concatenatedList // Переходим в меню с новым сцепленным списком
    | "8" -> 
        printfn "Выход из программы."
        ()
    | _ ->
        printfn "Некорректный ввод. Пожалуйста, выберите 1, 2, 3, 4, 5, 6, 7 или 8."
        showMenu list // Повторяем меню

    // Главная функция
[<EntryPoint>]
let main argv =
    let initialList = readListFromConsole ()
    showMenu initialList // Показать меню с первоначальным списком
    0 // Код завершения программы

