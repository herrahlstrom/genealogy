CREATE TABLE "sources" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "notes" TEXT NOT NULL,
    "page" TEXT NULL,
    "referenceId" TEXT NULL,
    "repository" TEXT NULL,
    "type" INTEGER NOT NULL,
    "url" TEXT NULL,
    "volume" TEXT NULL,
    CONSTRAINT "PK_sources" PRIMARY KEY ("id")
);
