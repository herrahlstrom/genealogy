CREATE TABLE "event_members" (
    "entityId" TEXT NOT NULL,
    "lifeStoryId" TEXT NOT NULL,
    "type" INTEGER NULL,
    "date" TEXT NULL,
    "endDate" TEXT NULL,
    CONSTRAINT "PK_event_members" PRIMARY KEY ("entityId", "lifeStoryId")
);