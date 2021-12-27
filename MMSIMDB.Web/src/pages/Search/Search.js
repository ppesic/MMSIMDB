import React from 'react';
import { connect } from 'react-redux';
import Grid from '@mui/material/Grid';
import { getMoviePage } from '../../actions/movieAction';
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';
import TextField from '@mui/material/TextField';
import MovieList from '../../components/Movie/MovieList';
import Button from '@mui/material/Button';

class Search extends React.Component{
    constructor(props){
        super(props);
        this.state = {
            search: "",
            pageNumber: 0,
            movieType: '1'
        }
    }
    searchChange = (e) =>{
        this.setState(
            {
                search:e.target.value,
                pageNumber: 0
            }, this.getData);
    }
    getData = () => {
        this.props.getMoviePage({
            search: this.state.search.length < 2 ? "" : this.state.search,
            pageNumber: this.state.pageNumber,
            movieTypeID: this.state.movieType
        });
    }
    changeMovieType = (event, newMovieType) => {
        if(newMovieType !== null){
            this.setState(
                {
                    movieType:newMovieType,
                    pageNumber: 0
                }, this.getData);
        }
      };
      moreClick = () => {
        this.setState(
            {
                pageNumber: this.state.pageNumber+1
            }, this.getData);
      }
    componentDidMount = () => {
        this.getData();
    }
    render(){
        return(
            <Grid 
                container 
                direction="column"
                alignItems="center"
            >
                <div>
                    <TextField             
                        margin="normal"
                        padding="normal"
                        label="Search"
                        placeholder="Search"
                        value={this.state.search}
                        onChange={this.searchChange}
                        style = {{width: 500}}
                    />
                </div>

                <div>
                    <ToggleButtonGroup
                        color="primary"
                        value={this.state.movieType}
                        exclusive
                        onChange={this.changeMovieType}
                        >
                        <ToggleButton value="1">Movie</ToggleButton>
                        <ToggleButton value="2">TV Series</ToggleButton>
                    </ToggleButtonGroup>
                </div>

                <MovieList></MovieList>
                <div style={{marginBottom:'20px'}}>
                    <Button 
                        variant="contained"
                        disabled={!this.props.status.hasMore}
                        onClick={this.moreClick}>
                        {this.props.status.hasMore ? "View more results" : "No more results"}
                    </Button>
                </div>
            </Grid>
        );
    }
}

const mapStateToProps = state => {
    return {status: state.status};
}

export default connect(mapStateToProps, { getMoviePage })(Search);