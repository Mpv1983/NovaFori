import React from 'react';
import { ToDoService } from '../services/todo-service';
import { Todo } from '../models/todo-model';
import './todo-list-component.css';

export interface ChildState {
    isLoadingComplete: boolean
  }

  interface MyProps{
    service :ToDoService
  }
export class ToDoList extends React.Component<MyProps, ChildState> {

    state: ChildState = { isLoadingComplete : false};
    listItemsPending: any;
    listItemsComplete: any;

    render() {
        
        if (!this.state.isLoadingComplete) {
            this.props.service.get().then((todoItems) => {

                this.populateLists(todoItems);

                this.setState({
                    isLoadingComplete:true
                });
            });
        }

        if (this.state.isLoadingComplete) {
            return (
                <div className="row">
                    <div className="section">
                        <h2>Pending</h2>
                        <ul>{this.listItemsPending}</ul>
                    </div>
                    <div className="section">
                        <h2>Complete</h2>
                        <ul>{this.listItemsComplete}</ul>
                    </div>
                </div>
            );
        }
        else {
            return <span> loading </span>
        }

      }

    update = () => {
        this.setState({
            isLoadingComplete: false
        });
    };

    populateLists(todoItems: Todo[]) {
        this.listItemsComplete = todoItems.map((item) => {
            if (item.isComplete) {
                return <li key={item.id}>{item.description}</li>
            }
        });

        this.listItemsPending = todoItems.map((item) => {
            if (!item.isComplete) {
                return <li key={item.id}>{item.description}</li>
            }
        });
    }
}