# State Management

**.NET Aspire** + **DAPR** - State Management

## For a written article, refer: [State Management](https://netrecipes.github.io/courses/dapr-aspire/state-management/)

- [x] `In-Memory` State Store

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: statestore
spec:
  type: state.in-memory
  version: v1
  metadata:
```

---

- [x] `Redis` State Store

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: statestore
spec:
  type: state.redis
  version: v1
  metadata:
  - name: redisHost
    value: localhost:6500
  - name: redisPassword
    value: "localDev"
  - name: keyprefix
    value: none
```

---
