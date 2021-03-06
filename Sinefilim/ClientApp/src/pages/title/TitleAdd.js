import React, { Component } from 'react';
import { Button, Input } from 'reactstrap';
import axios from 'axios';

export class TitleAdd extends Component {
  static displayName = TitleAdd.name;

  constructor(props) {
    super(props);
    this.state = { imdbUrl: '' };
    this.handleChange = this.handleChange.bind(this);
    this.addTitle = this.addTitle.bind(this);
  }

  async addTitle() {
    axios.post('api/titles', { imdbId: this.state.imdbUrl })
      .then(response => console.log(response));
  }

  handleChange(e) {
    this.setState({imdbUrl: e.target.value});    
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
