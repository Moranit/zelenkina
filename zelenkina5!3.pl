% Предикат для проверки принадлежности элемента к множеству
element(X, [X|_]).
element(X, [_|T]) :- element(X, T).

% Предикат для создания универсального множества
mnojestvolist(Mnojestvo, Mnojestvo) :- !.

% Предикат для нахождения дополнения множества
complement(Set, Mnojestvo, Complement) :-
    findall(X, (element(X, Mnojestvo), \+element(X, Set)), Complement).