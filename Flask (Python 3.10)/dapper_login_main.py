from flask import Flask, render_template, request, redirect, url_for, session, jsonify, flash
import requests
import json

app = Flask(__name__)
app.secret_key = "fake key"

@app.route('/', methods=['GET', 'POST'])
def first_endpoint():
    if 'token' not in session:
        return redirect(url_for('login'))
    else:
        return redirect(url_for('dashboard'))

# Login route
@app.route('/login', methods=['GET', 'POST'])
def login():
    if request.method == "POST":
        # Get username and password from the form
        username = request.form['username']
        email = request.form['email']
        password = request.form['password']
        
        # Send a POST request to the API for authentication
        url = 'https://localhost:7109/api/login'
        data = {'username': username, 'email': email, 'passwd': password}
        headers = {'Content-Type': 'application/json'}
        response = requests.post(url, data=json.dumps(data), headers=headers, verify=False)
        # print(jsonify(response))
        
        if response.status_code == 200:
            # Authentication successful, save the token in the session
            session['token'] = response.json()['token']
            print(session['token']) ###############################################
            return redirect(url_for('dashboard'))
        else:
            # Authentication failed, show error message
            error_message = 'Invalid username or password'
            return render_template('login.html', error=error_message)

    return render_template('login.html', error=None)

# Dashboard route
@app.route('/dashboard')
def dashboard():
    if 'token' not in session:
        # Redirect to login if token is not present in session
        return redirect(url_for('login'))

    # Fetch data from backend using token and display it in DisplayAll template
    url = 'https://localhost:7109/api/personauth'
    headers = {'Authorization': 'Bearer ' + session['token'], 'Content-Type': 'application/json'}
    response = requests.get(url, headers=headers, verify=False)
    persons = response.json()
    print(persons) #############################################################
    return render_template('displayall.html', persons=persons)

# Add route
@app.route('/person/add', methods=['GET', 'POST'])
def add_person():
    if request.method == 'POST':
        # Get form data
        name = request.form['name']
        email = request.form['email']
        address = request.form['address']

        # Create a new person object
        person = {'id': 0,'Name': name, 'Email': email, 'Address': address}

        # Send a POST request to the API to add the person
        url = 'https://localhost:7109/api/personauth'
        headers = {'Authorization': 'Bearer ' + session['token'], 'Content-Type': 'application/json'}
        response = requests.post(url, data=json.dumps(person), headers=headers, verify=False)

        if response.status_code == 200:
            return redirect(url_for('dashboard'))
        else:
            return render_template('add.html', error='Failed to add person')

    return render_template('add.html', error=None)

# Edit route
@app.route('/person/edit/<int:id>', methods=['GET', 'POST'])
def edit_person(id):
    # Fetch person details from the API
    url = 'https://localhost:7109/api/personauth/' + str(id)
    headers = {'Authorization': 'Bearer ' + session['token'], 'Content-Type': 'application/json'}
    response = requests.get(url, headers=headers, verify=False)
    # print(response) #############################################################
    # print(session['token']) #################################################
    person = response.json()
    print(person) #############################################################

    if request.method == 'POST':
        # Update the person details
        new_person = {}
        new_person['id'] = person['id']
        new_person['Name'] = request.form['name']
        new_person['Email'] = request.form['email']
        new_person['Address'] = request.form['address']


        # Send a PUT request to the API to update the person
        response = requests.put(url, data=json.dumps(new_person), headers=headers, verify=False)
        print("\tResponse of put: ",response) #############################################################

        if response.status_code == 200:
            return redirect(url_for('dashboard'))
        else:
            return render_template('edit.html', person=person, error='Failed to update person')

    return render_template('edit.html', person=person, error=None)

@app.route('/person/delete/<int:id>', methods=['GET'])
def delete_person(id):
    if 'token' not in session:
        return redirect(url_for('login'))

    url = f'https://localhost:7109/api/personauth/{id}'
    headers = {'Authorization': f'Bearer {session["token"]}'}
    response = requests.delete(url, headers=headers, verify=False)

    if response.status_code == 200:
        flash('Person deleted successfully', 'success')
    else:
        flash('Failed to delete person', 'error')

    return redirect(url_for('dashboard'))

if __name__ == '__main__':
    app.run(debug=True, port= 1234)
