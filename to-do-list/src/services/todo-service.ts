import { Todo } from '../models/todo-model'
import { Response, Request, newHttpClient } from 'typescript-http-client'
import { Outcome } from '../models/outcome';

export class ToDoService implements iToDoService{

    async get() :Promise<Todo[]>
    {

        // Get a new client
        const client = newHttpClient();
        // Build the request

        const request = new Request(process.env.REACT_APP_HOST_URL + '/todo/get');
        // Execute the request and get the response body as a "Todo" object
        const todoList = await client.execute<Todo[]>(request);

        return todoList;
    }

    async add(item: Todo): Promise<Outcome> {

        // Get a new client
        const client = newHttpClient();

        var postRequest = {
            contentType: 'application/json;',
            method: 'POST',
            withCredentials :false,
            body: item,
            headers: {},
            timeout: 30000,
            readyState: 0
        };

        // Build the request
        const request = new Request(process.env.REACT_APP_HOST_URL + '/todo/add', postRequest);

        // Execute the request and get the response body as a "Todo" object
        const outcome = await client.execute<Outcome>(request);

        return outcome;
    }

}

export interface iToDoService {
    get(): Promise<Todo[]>
    add(item:Todo): Promise<Outcome>
}