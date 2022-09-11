import { render, screen } from '@testing-library/react';
import { Outcome } from '../models/outcome';
import { Todo } from '../models/todo-model';
import { iToDoService } from '../services/todo-service';
import { ToDoList } from './todo-list-component';

test('List returns expected items', async () => {

    var itemOne = new Todo("1", "Buy Jag Parts", false);
    var itemTwo = new Todo("2", "Buy More Jag Parts", false);
    const list = [itemOne, itemTwo];

    let service: iToDoService = {
        get: jest.fn(async () => {
            return list;
        }),
        add: jest.fn(async (item) => {
            return new Outcome('',true);
        })
    }

    var sut = new ToDoList({ service: service });
    sut.populateLists(list);
    sut.state.isLoadingComplete = true;

    render(sut.render());

    const el = screen.getAllByRole('listitem');
    expect(el.length).toBe(2);

});
