export class Outcome {
    errorMessage: string;
    isSuccess: boolean;

    constructor(errorMessage: string, isSuccess: boolean) {
        this.errorMessage = errorMessage;
        this.isSuccess = isSuccess;
    }
}