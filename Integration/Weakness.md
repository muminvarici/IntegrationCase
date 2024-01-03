There were several options to achieve the distributed locking. I prefered to use redis lock.
There were some trade-offs or weaknesses while using distributed lock providers.

1. **Single Point of Failure:**
    - **Description:** If the Redis server becomes a single point of failure, the entire system will fail.
    - **Solution:** Implement a fallback mechanism in another technology like mongo.

2. **Security:**
   - **Description:** If the data is sensitive and if we put it into a public or as unencrypted form, there would be a potential risk.
   - **Solution:** Use secure connection or mask/encrypt data.

3. **Network Latency:**
    - **Description:** A redis call requires time to respond. And the time depends physical or logical cases
    - **Solution:** Move the lock provider into the same network(logical and physical) with the actual application if possible.

4. **Data Key Length:**
   - **Description:** I use `$"item_content_{itemContent}"`as redis key. If the content is too long, the system may ack unreliable.
   - **Solution:** Hash or use candidate keys. But that will cause another complexity.

5. **Lock Timeouts:**
    - **Description:** If we use another lock provider that doesn't support timeout, we need a custom solution to achieve the TTL or timeout 

