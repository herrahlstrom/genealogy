CREATE TABLE "media_meta" (
    "media_id" TEXT NOT NULL,
    "key" TEXT NOT NULL,
    "value" TEXT NOT NULL,
    CONSTRAINT "PK_media" PRIMARY KEY ("media_id", "key")
);
