CREATE TABLE "media" (
    "id" TEXT NOT NULL,
    "type" INTEGER NOT NULL,
    "path" TEXT NOT NULL,
    "size" INTEGER NULL,
    "title" TEXT NULL,
    "fileCrc" TEXT NULL,
    "notes" TEXT NULL,
    CONSTRAINT "PK_media" PRIMARY KEY ("id")
);
