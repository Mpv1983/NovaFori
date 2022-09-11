import React from 'react';
import { ToDoService } from '../services/todo-service';
import { Todo } from '../models/todo-model';
import { Outcome } from '../models/outcome';
import './todo-add-component.css';

export interface AddState {
    description: string
    errorMessage: string
}

interface MyProps {
    service: ToDoService
    onAddSuccessful: any
}
export class ToDoAdd extends React.Component<MyProps, AddState> {

    state: AddState = { description: '', errorMessage: '' };

    handleChange = (e: React.FormEvent<HTMLInputElement>): void => {
        this.setState({ description: e.currentTarget.value });
    };

    handleSubmit = (event: React.SyntheticEvent) => {
        
        this.props.service.add({ description: this.state.description, isComplete: false })
            .then((outcome: Outcome) => {
                this.setState({ description: '' });
                this.props.onAddSuccessful();
            })
            .catch((error: any) => {
                this.setState({ description: this.state.description, errorMessage: error.body.errorMessage });
            });
        event.preventDefault();
    }

    render() {

        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    New todo:
                    <input type="text" value={this.state.description} onChange={this.handleChange} />
                </label>
                <input type="submit" value="Submit" />
                <label className='error'>{this.state.errorMessage}</label>
            </form>
        );

    }

}