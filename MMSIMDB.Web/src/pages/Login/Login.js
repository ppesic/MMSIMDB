import * as React from 'react';
import { connect } from 'react-redux';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { login } from '../../actions/loginAction';
import TextField from '@mui/material/TextField';

const theme = createTheme();

class Login extends React.Component {
  constructor(props){
    super(props);
    this.state = {email: "", password: ""}
  } 
  submit = () => {
    this.props.login({email:this.state.email, password:this.state.password});
  };
  render(){
    return (
      <ThemeProvider theme={theme}>
        <Container component="main" maxWidth="sm">
          <CssBaseline />
          <Box
            sx={{
              marginTop: 8,
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
            }}
          >
            <Typography component="h3" variant="h4">
              Much, much simpler IMDB
            </Typography>
            <Typography component="h1" variant="h5">
              Sign in
            </Typography>
            
            <TextField             
              margin="normal"
              padding="normal"
              label="Email"
              placeholder="Email"             
              fullWidth
              value={this.state.email}
              onChange={(e) => {this.setState({email:e.target.value});}}
            />
            <TextField
             margin="normal"
             padding="normal"
             label="Password"
             placeholder="Password"            
             fullWidth
             type="password"
             value={this.state.password}
             onChange={(e) => {this.setState({password:e.target.value});}}
           />
           
              
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
                onClick={this.submit}
              >
                Sign In
              </Button>
            </Box>
        </Container>
      </ThemeProvider>
    )
  }
}

export default connect(null, { login })(Login);