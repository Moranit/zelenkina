// Функция для добавления элемента в список
let addElement element list =
    element :: list

// Функция для удаления элемента из списка
let removeElement element list =
    List.filter (fun x -> x <> element) list//проходит по списку и возвращает новый список с удаленным элементом

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

// Основная программа
[<EntryPoint>]
let main argv =
    // Пример использования функций
    let myList = [1; 2; 3; 4]
    printfn "Список: %A" myList

    // Добавление элемента
    let newList = addElement 0 myList
    printfn "Список после добавления: %A" newList

    // Удаление элемента
    let modifiedList = removeElement 3 newList
    printfn "Список после удаления: %A" modifiedList

    // Поиск элемента
    let elementToFind = 2
    if findElement elementToFind modifiedList then
        printfn "Элемент %d найден в списке." elementToFind
    else
        printfn "Элемент %d не найден в списке." elementToFind

    // Сцепка списков
    let anotherList = [5; 6; 7]
    let concatenatedList = concatenateLists modifiedList anotherList
    printfn "Сцепленный список: %A" concatenatedList

    // Получение элемента по индексу
    let indexToGet = 2
    match getElementAtIndex indexToGet concatenatedList with
    | Some element -> printfn "Элемент по индексу %d: %d" indexToGet element
    | None -> printfn "Индекс %d вне диапазона." indexToGet

    0 // Возвращаем код завершения программы

