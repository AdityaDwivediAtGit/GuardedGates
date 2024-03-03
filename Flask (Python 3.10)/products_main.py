from flask import Flask, render_template
import requests, json

app = Flask(__name__)

@app.route('/')
def index():
    # Send GET request to the API
    url = 'https://localhost:7216/api/Products'
    headers = {'accept': 'text/plain'}
    response = requests.get(url, headers=headers, verify=False)
    
    # Parse the response data
    if response.status_code == 200:
        products_json = json.loads(response.text)
        product_data = [(product['id'], product['name'], product['price']) for product in products_json]
        # Render the template with the product data
        return render_template('index.html', products=product_data)
    else:
        return f"Failed to fetch data. Status code: {response.status_code}"


if __name__ == '__main__':
    app.run(debug=True, port=1234)
