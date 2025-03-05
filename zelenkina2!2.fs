open System

let countElements (digit: char) (list: string list) = //параметры(цифра и список сторр), подсчитывает количество строк содреж цифру
    let containsDigit (s: string) = //доп функция - принимет s
        s.Contains(digit) //проверяет есть ли в строке цифра
    
    List.fold (fun acc item -> if containsDigit item then acc + 1 else acc) 0 list //проходимся по элементам, если есть цифра acc+1 иначе оставляем

[<EntryPoint>]
let main argv =
    //  цифра с консоли
    printf "Введите цифру для поиска: "
    let digitInput = Console.ReadLine()
    
    // Проверка корректности ввода
    if digitInput.Length = 1 then
        let n = digitInput.[0] // n для икомой цифры
        
        // список
        let numbers = ["123"; "456"; "789"; "12"; "34"; "56"]
        
        // Подсчёт и вывод результата
        let count = countElements n numbers
        printfn "Количество элементов списка, содержащих цифру %c: %d" n count
    else
        printfn "Ошибка: введите только одну цифру."
    
    0 // завершение
