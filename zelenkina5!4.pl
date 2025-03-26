marka(buick).
marka(chrysler).
marka(mustang).

cvet(blue).
cvet(black).

braun(Marka, Cvet) :-
    (Marka = buick, Cvet \= blue); 
    (Marka \= buick, Cvet = blue). 

dzhons(Marka, Cvet) :-
    (Marka = chrysler, Cvet \= black); 
    (Marka \= chrysler, Cvet = black). 


smit(Marka, Cvet) :-
    (Marka = mustang, Cvet \= blue); 
    (Marka \= mustang, Cvet = blue). 

result(Marka, Cvet) :-
    marka(Marka),
    cvet(Cvet),
    braun(Marka, Cvet),
    dzhons(Marka, Cvet),
    smit(Marka, Cvet).

