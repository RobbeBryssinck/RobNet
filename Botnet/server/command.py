from dataclasses import dataclass

@dataclass
class Command:
    commandId: int
    commandDescription: str
    userId: int
