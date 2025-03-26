countpos([], 0) :- !.
countpos([Head|_Tail], 0) :-
    Head =< 0, !.
countpos([_Head|Tail], Count) :-
    countpos(Tail, TailCount),
    Count is TailCount + 1.

max(A, B, A) :-
    A > B, !.
max(_, B, B).

max_count([], 0) :- !.
max_count([Head|Tail], Count) :-
    countpos([Head|Tail], ListCount),
    max_count(Tail, TailCount),
    max(ListCount, TailCount, Count).