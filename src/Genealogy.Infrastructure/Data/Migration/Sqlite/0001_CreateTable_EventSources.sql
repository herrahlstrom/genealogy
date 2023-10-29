CREATE TABLE "event_sources" (
    "eventId" TEXT NOT NULL,
    "sourceId" TEXT NOT NULL,
    CONSTRAINT "PK_event_sources" PRIMARY KEY ("eventId", "sourceId")
);
