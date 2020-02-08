import React, { Component } from 'react';
import { ListGroup, ListGroupItem, ListGroupItemHeading, ListGroupItemText } from 'reactstrap';
import { Link } from 'react-router-dom';
import axios from 'axios';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { titles: [], loading: true };
  }
  
  componentDidMount() {
    this.fetchTitles();
  }

  async fetchTitles() {
      axios.get('/api/titles/').then(({ data }) => {
        this.setState({ titles: data, loading: false });
      });
  }

  static renderTitles(titles) {
    return (
      <ListGroup>
        {titles.map(title => 
          <ListGroupItem key={title.id} tag="a"  tag={Link} to={"/title/" + title.id}>
            <ListGroupItemHeading>{title.name}</ListGroupItemHeading>
            <ListGroupItemText>İçerik</ListGroupItemText>
          </ListGroupItem>
        )}
      </ListGroup>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Home.renderTitles(this.state.titles);

    return (
      <div>
        <h1>Filmler</h1>
        {contents}
      </div>
    );
  }
}
