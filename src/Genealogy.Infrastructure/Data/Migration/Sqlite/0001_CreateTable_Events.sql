CREATE TABLE "events" (
    "id" TEXT NOT NULL CONSTRAINT "PK_events" PRIMARY KEY,
    "date" TEXT NULL,
    "endDate" TEXT NULL,
    "location" TEXT NULL,
    "name" TEXT NULL,
    "notes" TEXT NOT NULL,
    "type" INTEGER NOT NULL
);
