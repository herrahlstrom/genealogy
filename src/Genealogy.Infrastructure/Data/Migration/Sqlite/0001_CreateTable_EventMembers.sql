CREATE TABLE "event_members" (
    "eventId" TEXT NOT NULL,
    "entityId" TEXT NOT NULL,
    "type" INTEGER NOT NULL,
    "date" TEXT NULL,
    "endDate" TEXT NULL,
    CONSTRAINT "PK_event_members" PRIMARY KEY ("eventId", "entityId")
);