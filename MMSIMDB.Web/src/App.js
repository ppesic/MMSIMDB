import React from 'react';
import logo from './logo.svg';
import './App.css';
import Search from './pages/Search/Search';
import Login from './pages/Login/Login';
import { connect } from 'react-redux';
import { getUserInfo } from './actions/loginAction';
import { withSnackbar } from 'notistack';

class App extends React.Component {
  componentDidMount = () =>{
    this.props.getUserInfo();
  }
  layout = () => {
    if(this.props.authUser.isAuth){
      return  <Search></Search>
    }
    else{
      return <Login></Login>
    }
  }
  componentDidUpdate(prevProps, prevState, snapshot) {
      if (prevProps.errors !== this.props.errors) { 
        if(this.props.errors?.message !== null && this.props.errors?.message !== ''){
          //consle.log(this.props.errors.message);
          this.props.errors.message.split('<br/>').forEach(element => 
            this.props.enqueueSnackbar(element, {variant:'error', autoHideDuration:7000}) //default | error | success | warning | info
            );
        }
      }
  }
  render(){
    return this.layout();
  }
}


const mapStateToProps = state => {
  return { authUser: state.authUser,
  errors: state.errors };
}

export default connect(mapStateToProps, { getUserInfo } )(withSnackbar(App));
