**ETAP I**

1. Sklonuj projekt z github.com i wejdź do katalogu projektu

git clone adres_repozytorium
cd /nazwa_repozytorium

**2. Uruchom Docker Compose**

a) Sprawdź uruchomione kontenery
sudo docker-compose ps 

b) Jeśli nie ma żadnych kontenerów na liście, zbuduj projekt
sudo docker-compose build

c) Uruchom w tle serwisy zaimplementowane w Docker Compose - API i DB
sudo docker-compose up -d

**ETAP II**
Podłączenie bazdy danych PostgreSQL i utworzenie tabeli wraz z kolumnami

**1) Zaloguj się do kontenera z PostgreSQL**
sudo docker-compose exec db psql -U demo -d demo

**2) Utwórz tabelę w bazie danych**
CREATE TABLE "Todos" (
  "Id" SERIAL PRIMARY KEY,
  "Title" TEXT NOT NULL,
  "IsDone" BOOLEAN NOT NULL,
  "CreatedAt" TIMESTAMPTZ NOT NULL
);

**3) Dodaj dodatkową kolumnę na priorytet**
ALTER TABLE "Todos" ADD COLUMN "Priority" INT DEFAULT 1;


4) Wyloguj się z bazy danych i kontenera
\q

