import React, { Component } from 'react';
import { Link } from 'react-router-dom'

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { todos: [], loading: true };
    }

    componentDidMount() {
        this.populateTodoData();
    }

    async populateTodoData() {
        const response = await fetch('api/todo');
        const data = await response.json();
        this.setState({ todos: data, loading: false });
    }

    static renderTodosTable(todos) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>State</th>
                        <th style={{ width: '50px' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {todos.map(todo =>
                        <tr key={todo.id}>
                            <td>{todo.title}</td>
                            <td>{todo.toDoState}</td>
                            <th style={{ width: '50px' }}><Link to={{ pathname: "/todo-editor", search: `?id=${todo.id}` }} className="btn btn-primary">Edit</Link></th>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderTodosTable(this.state.todos);

        return (
            <div>
                <h1 id="tabelLabel" >Todo Items</h1>
                {contents}
            </div>
        );
    }
}
