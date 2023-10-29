CREATE TABLE "persons" (
    "id" TEXT NOT NULL CONSTRAINT "PK_persons" PRIMARY KEY,
    "name" TEXT NOT NULL,
    "notes" TEXT NULL,
    "profession" TEXT NULL,
    "sex" TEXT NOT NULL
);
