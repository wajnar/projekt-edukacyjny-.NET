# 🧪 REST API – .NET + Docker + PostgreSQL (TODO z priorytetem)

Prosty projekt edukacyjny pokazujący, jak:

- uruchomić **REST API w .NET** w kontenerze Dockera,  
- połączyć się z bazą **PostgreSQL** w drugim kontenerze,  
- utworzyć tabelę **Todos** z kolumną **Priority**,  
- korzystać z API jako ćwiczenia dla uczniów.

Projekt przeznaczony jest jako **ćwiczenie dla technikum informatycznego**.

---

## 🧱 Technologie

- .NET 9 (minimal API)
- PostgreSQL 16 (Docker)
- Docker + Docker Compose
- HTML + JavaScript (prosty frontend)

---

## ⚙️ Wymagania wstępne

- Zainstalowany **Docker** oraz **Docker Compose**
- System Linux / WSL / inne środowisko zgodne z Dockerem

---

## ▶️ ETAP I – Uruchomienie kontenerów (API + DB)

1. **Sklonuj projekt i wejdź do katalogu:**

   ```bash
   git clone https://github.com/adrianflak/restapi_dotnet_priority.git
   cd restapi_dotnet_priority
   docker-compose build
   docker-compose up -d


2. **Utwórz bazę danych:**

   ```bash
   sudo docker-compose exec db psql -U demo -d demo

   CREATE TABLE "Todos" (
        "Id" SERIAL PRIMARY KEY,
        "Title" TEXT NOT NULL,
        "IsDone" BOOLEAN NOT NULL,
        "CreatedAt" TIMESTAMPTZ NOT NULL
    );
   ALTER TABLE "Todos" ADD COLUMN "Priority" INT DEFAULT 1;