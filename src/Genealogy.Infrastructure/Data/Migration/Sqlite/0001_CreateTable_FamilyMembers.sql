CREATE TABLE "family_members" (
    "familyId" TEXT NOT NULL,
    "personId" TEXT NOT NULL,
    "memberType" INTEGER NOT NULL,
    CONSTRAINT "PK_family_members" PRIMARY KEY ("familyId", "personId")
);
