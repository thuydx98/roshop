import { isNil } from 'lodash/fp';
import moment from 'moment';

class CartItem {
  static propTypes = {
    productId: Number,
    quantity: Number,
    addedAt: String,
  };

  constructor(data) {
    if (!isNil(data)) {
      const { productId, quantity, addedAt } = data;
      this.productId = productId;
      this.quantity = quantity;
      this.addedAt = addedAt || moment().format();
    }
  }
}

export default CartItem;
