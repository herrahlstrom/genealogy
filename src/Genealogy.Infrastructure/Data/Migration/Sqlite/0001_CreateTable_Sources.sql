CREATE TABLE "sources" (
    "id" TEXT NOT NULL CONSTRAINT "PK_sources" PRIMARY KEY,
    "name" TEXT NULL,
    "notes" TEXT NULL,
    "page" TEXT NULL,
    "referenceId" TEXT NULL,
    "repository" TEXT NULL,
    "type" INTEGER NOT NULL,
    "url" TEXT NULL,
    "volume" TEXT NULL
);
