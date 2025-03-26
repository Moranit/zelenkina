
reverse_digits(N, M) :-
    integer(N),
    reverse_digits(N, 0, M).

% Базовый случай: если число равно нулю, возвращаем аккумулятор
reverse_digits(0, M, M) :- !.

% Рекурсивный случай: извлекаем цифру и рекурсивно обрабатываем остаток
reverse_digits(N, M0, M) :-
    N > 0,
    R is N div 10,           % Получаем целую часть от деления на 10
    LastDigit is N mod 10,   % Извлекаем последнюю цифру
    M1 is M0 * 10 + LastDigit, % Добавляем цифру в аккумулятор
    reverse_digits(R, M1, M).
