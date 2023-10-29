CREATE TABLE "media_reference" (
    "mediaId" TEXT NOT NULL,
    "entityId" TEXT NOT NULL,
    CONSTRAINT "PK_media_reference" PRIMARY KEY ("mediaId", "entityId")
);
