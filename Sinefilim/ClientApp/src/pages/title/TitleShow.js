import React, { Component } from 'react';
import { Button, Input } from 'reactstrap';
import axios from 'axios';

export class TitleShow extends Component {
  static displayName = TitleShow.name;

  constructor(props) {
    super(props);
    this.state = { loading: true };
  }

  componentDidMount() {
    this.fetchTitle();
  }

  async fetchTitle() {
      const response = axios.get('/api/titles/')
  }

  render() {
    return (
      <div>
        <h4>Film Ekle</h4>
        <Input type="text" value={this.state.imdbUrl} onChange={this.handleChange} name="imdbUrl" placeholder="Eklenmesini istediğiniz filmin IMDb url'sini giriniz." />
        <Button color="primary" onClick={this.addTitle}>Ekle</Button>
      </div>
    );
  }
}
