PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS `users` (
    `id` INTEGER PRIMARY KEY,
    `name` TEXT NOT NULL,
    `permissionLevel` INTEGER NOT NULL,
    `phone` TEXT NOT NULL,
    `photo` BLOB,
    `cardId` TEXT UNIQUE,
    `bluetoothId` INTEGER UNIQUE,
    `bluetoothName` TEXT,
    `pin` TEXT,
    CHECK (
        (`cardId` IS NOT NULL AND `bluetoothId` IS NOT NULL AND `pin` IS NULL)
        OR (`cardId` IS NOT NULL AND `bluetoothId` IS NULL AND `pin` IS NOT NULL)
        OR (`cardId` IS NULL AND `bluetoothId` IS NOT NULL AND `pin` IS NOT NULL)
    ),
    CHECK ((`bluetoothId` IS NULL) == (`bluetoothName` IS NULL))
);

CREATE TABLE IF NOT EXISTS `auditLog` (
    `datetime` INTEGER NOT NULL,
    `message` TEXT NOT NULL
);
