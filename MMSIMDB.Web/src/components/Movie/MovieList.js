import * as React from 'react';
import { connect } from 'react-redux';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import Divider from '@mui/material/Divider';
import ListItemText from '@mui/material/ListItemText';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';
import Rating from '@mui/material/Rating';
import {addMovieRating} from '../../actions/movieAction'

class MovieList extends React.Component{
    starClick = (movieID, rating) => {
        this.props.addMovieRating({
            movieID: movieID,
            rating: rating ?? 0
        });
    }
    getListItems = () => {
        return this.props.movies.map(item => (
            <ListItem alignItems="center" key={item.id} >
                <ListItemAvatar>
                    <Avatar 
                    alt={item.title} 
                    src={"https://localhost:7192/api/v1/movie/GetImage/" + item.imageName} 
                    variant="rounded"
                    sx={{ width: 45, height: 67 }}
                    />
                </ListItemAvatar>
                <ListItemText
                    primary={item.title}
                    secondary={
                    <React.Fragment>
                        <Typography
                        mx={{ display: 'inline' }}
                        component="span"
                        variant="body2"
                        color="text.primary"
                        >
                            Description: {item.description} ({item.year})                        
                        </Typography>
                        <Typography 
                            mx={{ display: 'block' }}
                            component="span"
                            variant="body2"
                            color="text.primary">
                            Rating: {item.numberOfRatings === 0 ? 0 : (item.ratingSum / item.numberOfRatings).toFixed(2)}
                        </Typography>                 
                    </React.Fragment>                    
                    }
                />
                <Rating
                    name="simple-controlled"
                    value={item.myRating}
                    onChange={(event, newValue) => {
                        this.starClick(item.id, newValue);
                    }}
                />
            </ListItem>
        ));
    }
    render(){
        return (
            <List sx={{ width: '100%', maxWidth: 500, bgcolor: 'background.paper' }}>
                {this.getListItems()}
            </List>
        );
    }
}

const mapStateToProps = state => {
    return {movies: state.movies};
}

export default connect(mapStateToProps, { addMovieRating })(MovieList);