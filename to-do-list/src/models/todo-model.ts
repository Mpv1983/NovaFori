export class Todo {
    id?: string;
    description: string;
    isComplete: boolean;

    constructor(id: string, description: string, isComplete: boolean){
        this.id = id;
        this.description = description;
        this.isComplete = isComplete;
    }
}