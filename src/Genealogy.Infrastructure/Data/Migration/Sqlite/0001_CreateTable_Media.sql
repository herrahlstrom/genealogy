CREATE TABLE "media" (
    "id" TEXT NOT NULL CONSTRAINT "PK_media" PRIMARY KEY,
    "type" INTEGER NOT NULL,
    "path" TEXT NOT NULL,
    "size" INTEGER NOT NULL,
    "title" TEXT NULL,
    "fileCrc" TEXT NULL,
    "notes" TEXT NULL
);
