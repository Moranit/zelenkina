
reverse_digits(N, M) :-
    integer(N),
    reverse_digits(N, 0, M).

reverse_digits(0, M, M) :- !.

reverse_digits(N, M0, M) :-
    N > 0,
    R is N div 10,           
    LastDigit is N mod 10,   
    M1 is M0 * 10 + LastDigit, 
    reverse_digits(R, M1, M).
